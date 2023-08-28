using FiapStoreMinimalApi.Entidade;
using FiapStoreMinimalApi.Interface;
using FiapStoreMinimalApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/buscar-todos-usuario", (IUsuarioRepository usuarioRepository) =>
{
    return usuarioRepository.ObterTodosUsuarios();
});

app.MapGet("/buscar-usuario/{id}", (int id, IUsuarioRepository usuarioRepository) =>
{
    return usuarioRepository.ObterUsuarioPorId(id);
});

app.MapPost("/usuario", (Usuario usuario, IUsuarioRepository usuarioRepository) =>
{
    usuarioRepository.CadastrarUsuario(usuario);
});

app.MapPut("/usuario", (Usuario usuario, IUsuarioRepository usuarioRepository) =>
{
    usuarioRepository.AlterarUsuario(usuario);
});

app.MapDelete("/usuario", (int id, IUsuarioRepository usuarioRepository) =>
{
    usuarioRepository.DeletarUsuario(id);
});

app.Run();
