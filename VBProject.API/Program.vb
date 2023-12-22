Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.OpenApi.Models
Imports VBProject.Business
Imports VBProject.DataAccess
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports Microsoft.IdentityModel.Tokens
Imports Microsoft.AspNetCore.Authentication.JwtBearer
Imports System.Text

Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        ' Add services to the container.
        builder.Services.AddControllers()
        ' Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())

        builder.Services.AddHttpContextAccessor()
        builder.Services.AddDbContext(Of ErpDB2Context)()
        builder.Services.AddScoped(Of IProductService, ProductManager)()
        builder.Services.AddScoped(Of IBrandService, BrandManager)()
        builder.Services.AddScoped(Of ICompanyService, CompanyManager)()
        builder.Services.AddScoped(Of IDepartmentService, DepartmentManager)()
        builder.Services.AddScoped(Of IInvoiceService, InvoiceManager)()
        builder.Services.AddScoped(Of IInvoiceDetailService, InvoiceDetailManager)()
        builder.Services.AddScoped(Of IOfferService, OfferManager)()
        builder.Services.AddScoped(Of IRequestService, RequestManager)()
        builder.Services.AddScoped(Of IRoleService, RoleManager)()
        builder.Services.AddScoped(Of IStockDetailService, StockDetailManager)()
        builder.Services.AddScoped(Of IStockService, StockManager)()
        builder.Services.AddScoped(Of IUserService, UserManager)()
        builder.Services.AddScoped(Of IUserRoleService, UserRoleManager)()
        builder.Services.AddScoped(Of IUnitOfWork, UnitOfWork)()
        builder.Services.AddScoped(Of ICategoryService, CategoryManager)()

        builder.Services.AddHttpContextAccessor()

        builder.Services.AddSwaggerGen(Sub(c)
                                           c.SwaggerDoc("v1", New OpenApiInfo With {
        .Title = "JwtTokenWithIdentity",
        .Version = "v1",
        .Description = "JwtTokenWithIdentity test app"
    })
                                           c.AddSecurityDefinition("Bearer", New OpenApiSecurityScheme() With {
        .Name = "Authorization",
        .Type = SecuritySchemeType.ApiKey,
        .Scheme = "Bearer",
        .BearerFormat = "JWT",
        .In = ParameterLocation.Header,
        .Description = "Enter 'Bearer' [space] and then your valid token in the text input below." & vbCrLf &
                       "Example: ""Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"""
    })

                                           c.AddSecurityRequirement(New OpenApiSecurityRequirement From {
        {
            New OpenApiSecurityScheme With {
                .Reference = New OpenApiReference With {
                    .Type = ReferenceType.SecurityScheme,
                    .Id = "Bearer"
                }
            },
            New String() {}
        }
    })
                                       End Sub)



        builder.Services.AddAuthentication(Sub(opt)
                                               opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme
                                               opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme
                                           End Sub) _
        .AddJwtBearer(Sub(options)
                          options.IncludeErrorDetails = True
                          options.TokenValidationParameters = New TokenValidationParameters With {
        .ValidateIssuer = True,
        .ValidateAudience = True,
        .ValidateLifetime = True,
        .ValidateIssuerSigningKey = True,
        .ValidIssuer = builder.Configuration("JWT:Issuer"), ' Tokený oluþturan tarafýn adresi
        .ValidAudience = builder.Configuration("JWT:Audiance"), ' Tokenýn kullanýlacaðý hedef adres
        .IssuerSigningKey = New SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration("Jwt:Token"))) ' Gizli anahtar
    }
                      End Sub)

        Dim app = builder.Build()

        ' Configure the HTTP request pipeline.
        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If

        app.UseHttpsRedirection()
        app.UseCors(Sub(options) options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
        app.UseAuthentication()
        app.UseAuthorization()
        app.MapControllers()
        app.Run()
        app.UseSession()

        app.Run()
    End Sub
End Module

