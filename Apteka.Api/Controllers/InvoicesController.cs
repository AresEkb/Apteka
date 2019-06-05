using System;
using System.Linq;

using Apteka.Model.Dtos;
using Apteka.Model.EFCore;
using Apteka.Model.Entities;
using Apteka.Model.Factories;
using Apteka.Model.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apteka.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        protected readonly AptekaDbContext _context;

        public InvoicesController(AptekaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Запрос одной записи по идентификатору
        /// </summary>
        /// <response code="200">Запись найдена</response>
        /// <response code="404">Запись с указанным идентификатором не найдена</response>
        /// <response code="500">Произошла серверная ошибка</response>
        [HttpGet("{guid}")]
        [Produces("application/xml")]
        public ActionResult<InvoiceXml> GetByGuid(Guid guid)
        {
            var result = GetEntities().FirstOrDefault(entity => entity.Guid == guid);
            if (result == null)
            {
                return new NotFoundResult();
            }
            var mapper = new InvoiceXmlMapper(new PlainEntityFactory());
            var invoiceXml = mapper.Map(result);
            return invoiceXml;
        }

        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <response code="201">Запись успешно добавлена</response>
        /// <response code="400">Запись не может быть добавлена, т.к. предоставлены не корректные данные</response>
        /// <response code="500">Произошла серверная ошибка</response>
        [HttpPost]
        [Consumes("application/xml")]
        public IActionResult Create(InvoiceXml invoiceXml)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mapper = new InvoiceXmlMapper(new EntityFactory(_context));
            var entity = mapper.Map(invoiceXml);

            OnCreating(entity);
            _context.Set<Invoice>().Add(entity);
            _context.SaveChanges();
            //return CreatedAtAction(nameof(GetByGuid), new { guid = entity.Guid }, entity);
            return Ok();
        }

        protected virtual IQueryable<Invoice> GetEntities() =>
            _context.Set<Invoice>()//.Select(entity => OnRead(entity))
                .Include(entity => entity.Supplier)
                .Include(entity => entity.SupplierBankAccount)
                .Include(entity => entity.Receiver)
                .Include(entity => entity.Consignee)
                .Include(entity => entity.Items)
                    .ThenInclude(item => item.Manufacturer)
                .Include(entity => entity.Items)
                    .ThenInclude(item => item.ManufacturerCountry)
                .Include(entity => entity.Items)
                    .ThenInclude(item => item.Series);

        //protected virtual Invoice OnRead(Invoice entity)
        //{
        //    return entity;
        //}

        protected virtual void OnCreating(Invoice entity)
        {
        }
    }
}
