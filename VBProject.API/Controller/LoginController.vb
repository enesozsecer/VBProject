Imports System.IdentityModel.Tokens.Jwt
Imports System.Security.Claims
Imports System.Text
Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Configuration
Imports Microsoft.IdentityModel.Tokens
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class LoginController
    Inherits Controller
    Private ReadOnly _userService As IUserService
    Private ReadOnly _userRoleService As IUserRoleService
    Private ReadOnly _configuration As IConfiguration
    Private ReadOnly _mapper As IMapper

    Public Sub New(userService As IUserService, userRoleService As IUserRoleService, configuration As IConfiguration, mapper As IMapper)
        _userService = userService
        _userRoleService = userRoleService
        _configuration = configuration
        _mapper = mapper
    End Sub

    <HttpPost("/Login")>
    Public Async Function LoginAsync(loginRequestDTO As LoginRequestDTO) As Task(Of IActionResult)

        Dim user = Await _userService.GetAsync(Function(x) x.Email = loginRequestDTO.KullaniciAdi, "Department.Company", "UserRoles.Role")

        If user Is Nothing Then
            Return Ok(Sonuc(Of LoginResponseDTO).SuccessNoDataFound())
        End If

        If BCrypt.Net.BCrypt.Verify(loginRequestDTO.Sifre, user.Password) Then
            Dim claims As New List(Of Claim) From {
                New Claim(ClaimTypes.Name, user.Name),
                New Claim(ClaimTypes.Surname, user.LastName),
                New Claim(ClaimTypes.Email, user.Email),
                New Claim("EmailForMW", user.Email)
            }

            For Each item In user.UserRoles
                claims.Add(New Claim(ClaimTypes.Role, item.Role.Name))
            Next

            Dim secretKey = _configuration("JWT:Token")
            Dim issuer = _configuration("JWT:Issuer")
            Dim audiance = _configuration("JWT:Audiance")

            Dim tokenHandler = New JwtSecurityTokenHandler()
            Dim key = Encoding.UTF8.GetBytes(secretKey)
            Dim tokenDescriptor As New SecurityTokenDescriptor With {
                .Audience = audiance,
                .Issuer = issuer,
                .Subject = New ClaimsIdentity(claims),
                .Expires = DateTime.Now.AddDays(20),
                .SigningCredentials = New SigningCredentials(New SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            }

            Dim token = tokenHandler.CreateToken(tokenDescriptor)

            Dim loginResponseDTO As New LoginResponseDTO With {
                .AdSoyad = $"{user.Name} {user.LastName}",
                .EPosta = user.Email,
                .Token = tokenHandler.WriteToken(token),
                .UserId = user.Id,
                .RoleName = user.UserRoles.Select(Function(e) e.Role.Name).ToList(),
                .CompanyId = user.Department.CompanyId,
                .DepartmentId = user.DepartmentId,
                .DepartmentName = user.Department.Name,
                .CompanyName = user.Department.Company.Name
            }

            Return Ok(Sonuc(Of LoginResponseDTO).SuccessWithData(loginResponseDTO))
        End If

        Return Ok(Sonuc(Of LoginResponseDTO).SuccessNoDataFound())

    End Function
End Class
