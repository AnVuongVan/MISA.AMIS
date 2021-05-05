using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace MISA.AMIS.Controllers
{
    /// <summary>
    /// Api base chung
    /// </summary>
    /// <typeparam name="T">Đối tượng tương ứng trong entity</typeparam>
    /// CreatedBy: VVAn(22/04/2021)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<T> : ControllerBase
    {
        IBaseService<T> _baseService;

        public BaseEntityController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }

        [HttpGet]       
        [Authorize]
        public IActionResult Get()
        {
            var entities = _baseService.Get();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Get(string id)
        {
            var entity = _baseService.GetById(Guid.Parse(id));
            return Ok(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Post(T t)
        {
            var serviceResult = _baseService.Add(t);
            if (serviceResult.MISACode == MISACode.NotValid)
                return BadRequest(serviceResult);
            return Ok(serviceResult);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Put([FromRoute] string id, T t)
        {
            var keyProperty = t.GetType().GetProperty($"{typeof(T).Name}Id");
            if (keyProperty.PropertyType == typeof(Guid))
            {
                keyProperty.SetValue(t, Guid.Parse(id));
            }
            else if (keyProperty.PropertyType == typeof(int))
            {
                keyProperty.SetValue(t, int.Parse(id));
            }
            else
            {
                keyProperty.SetValue(t, id);
            }

            var serviceResult = _baseService.Update(t);
            if (serviceResult.MISACode == MISACode.NotValid)
                return BadRequest(serviceResult);
            return Ok(serviceResult);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(string id)
        {
            var rowEffects = _baseService.Delete(Guid.Parse(id));
            PropertyInfo data = rowEffects.GetType().GetProperty("Data");
            int isDeleted = (int) data.GetValue(rowEffects, null);

            if (isDeleted == 0)
            {
                return NoContent();
            }
            return Ok(rowEffects);
        }

        protected string GetPositionId()
        {
            return this.User.Claims.First(i => i.Type == "PositionId").Value;
        }
    }
}
