﻿using FiapStore.Enums;

namespace FiapStore.Dto
{
    public class CadastrarUsuarioDto
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
    }
}
