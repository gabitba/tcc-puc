﻿using ModuloInformacoesCadastrais.Application.DTOs;

namespace ModuloInformacoesCadastrais.Application.Interfaces
{
    public interface IClientesServico
    {
        Task<IEnumerable<ClienteDTO>> ObterClientesAsync();

        Task<ClienteDTO> ObterClienteAsync(int id);

        Task<int> CadastrarClienteAsync(ClienteDTO cliente);

        Task<bool> RemoverClienteAync(int id);
    }
}
