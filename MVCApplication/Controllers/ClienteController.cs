using AutoMapper;
using BusinessLogicalLayer;
using BusinessLogicalLayer.Interfaces;
using Common;
using Metadata;
using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace MVCApplication.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DataResponse<Cliente> dados = await _service.GetClientes();
            List<ClienteQueryViewModel> data = _mapper.Map<List<ClienteQueryViewModel>>(dados.Data);
            return View(data);
        }

        //meusite.com/Cliente/Edit/Ronaldo
        //[ResponseCache(VaryByHeader = "QueryString", Duration = 3600)]
        //InMemoryCache
        //redis
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            //Repare na existencia do id.Value (devido ao uso do recuro "?", conhecido como Nullable)
            SingleResponse<Cliente> response = await this._service.GetById(id.Value);
            if (response.HasSuccess)
            {
                Cliente cliente = response.Item;
                ClienteUpdateViewModel viewModel = _mapper.Map<ClienteUpdateViewModel>(cliente);
                return View(viewModel);
            }
            ViewBag.Error = response.Message;
            return View(null);
        }

        //Atenção aos hiddenfields, deve-se criar algum tipo de validação para garantir que estes não foram
        //alterados, pode-se fazer via javascript ou via sessão.
        [HttpPost]
        public async Task<IActionResult> Edit(ClienteUpdateViewModel viewmodel)
        {
            Cliente cliente = _mapper.Map<Cliente>(viewmodel);
            Response response = await _service.Update(cliente);
            if (!response.HasSuccess)
            {
                ViewBag.Error = response.Message;
                return View(viewmodel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //DataAnnotation | Attribute
        [HttpPost]
        public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
        {
            Response response = await _service.Insert(_mapper.Map<Cliente>(viewModel));
            if (!response.HasSuccess)
            {
                ViewBag.Error = response.Message;
                return View();
            }

            return RedirectToAction("Index");
        }
    }


    class CustomAutoMapper<T, W> where W : new()
    {
        public static W MapTo(T item)
        {
            W w = new W();

            foreach (var property in typeof(T).GetProperties())
            {
                PropertyInfo? propertyTarget = typeof(W).GetProperty(property.Name);
                if (propertyTarget != null)
                {
                    propertyTarget.SetValue(w, property.GetValue(item));
                }
            }
            return w;
        }
    }
}
