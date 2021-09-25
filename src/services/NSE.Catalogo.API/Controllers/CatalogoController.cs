﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Controllers
{

    [Authorize]
    [Route("")]
    [Route("Vitrine")]
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            this._produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("/catalogo/produtos")]
        public async Task<IEnumerable<Produto>> Index()
        {
            return await this._produtoRepository.ObterTodos();
        }

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("/catalogo/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await this._produtoRepository.ObterPorId(id);
        }
    }
}
