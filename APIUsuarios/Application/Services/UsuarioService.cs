using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APIUsuarios.Application.DTOs;
using APIUsuarios.Application.Interfaces;
using APIUsuarios.Domain.Entities;

namespace APIUsuarios.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo) => _repo = repo;

        public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
        {
            var emailNormalized = dto.Email.Trim().ToLowerInvariant();
            if (await _repo.EmailExistsAsync(emailNormalized, ct))
                throw new InvalidOperationException("Email já cadastrado");

            var usuario = new Usuario
            {
                Nome = dto.Nome.Trim(),
                Email = emailNormalized,
                Senha = dto.Senha,
                DataNascimento = dto.DataNascimento,
                Telefone = dto.Telefone,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            await _repo.AddAsync(usuario, ct);
            await _repo.SaveChangesAsync(ct);

            return new UsuarioReadDto(usuario.Id, usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Telefone, usuario.Ativo, usuario.DataCriacao);
        }

        public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
        {
            return await _repo.EmailExistsAsync(email.Trim().ToLowerInvariant(), ct);
        }

        public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u == null) return null;
            return new UsuarioReadDto(u.Id, u.Nome, u.Email, u.DataNascimento, u.Telefone, u.Ativo, u.DataCriacao);
        }

        public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(u => new UsuarioReadDto(u.Id, u.Nome, u.Email, u.DataNascimento, u.Telefone, u.Ativo, u.DataCriacao));
        }

        public async Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u == null) throw new KeyNotFoundException("Usuário não encontrado");

            var emailNormalized = dto.Email.Trim().ToLowerInvariant();
            if (u.Email != emailNormalized && await _repo.EmailExistsAsync(emailNormalized, ct))
                throw new InvalidOperationException("Email já cadastrado");

            u.Nome = dto.Nome.Trim();
            u.Email = emailNormalized;
            u.DataNascimento = dto.DataNascimento;
            u.Telefone = dto.Telefone;
            u.Ativo = dto.Ativo;
            u.DataAtualizacao = DateTime.UtcNow;

            await _repo.UpdateAsync(u, ct);
            await _repo.SaveChangesAsync(ct);

            return new UsuarioReadDto(u.Id, u.Nome, u.Email, u.DataNascimento, u.Telefone, u.Ativo, u.DataCriacao);
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken ct)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u == null) return false;
            u.Ativo = false;
            u.DataAtualizacao = DateTime.UtcNow;
            await _repo.RemoveAsync(u, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
