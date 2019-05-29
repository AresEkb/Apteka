using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apteka.Api.Controllers.Base
{

    [Route("api/[controller]")]
    [ApiController]
    public abstract class EntityController<TContext, TEntity> : Controller
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        protected readonly TContext _context;

        public EntityController(TContext context)
        {
            _context = context;
        }

        //public override Type EntityType { get => typeof(TEntity); }

        /// <summary>
        /// Запрос всех записей справочника
        /// </summary>
        /// <response code="200">Все записи справочника</response>
        /// <response code="500">Произошла серверная ошибка</response>
        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> GetAll() =>
            new OkObjectResult(GetEntities());

        /// <summary>
        /// Запрос одной записи справочника по идентификатору
        /// </summary>
        /// <response code="200">Запись найдена</response>
        /// <response code="404">Запись с указанным идентификатором не найдена</response>
        /// <response code="500">Произошла серверная ошибка</response>
        [HttpGet("{id}")]
        public ActionResult<TEntity> GetById(int id)
        {
            var result = GetEntities().FirstOrDefault(entity => entity.Id == id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }

        protected virtual IQueryable<TEntity> GetEntities() =>
            _context.Set<TEntity>().Select(entity => OnRead(entity));

        protected virtual TEntity OnRead(TEntity entity)
        {
            return entity;
        }
    }
}
