﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using System;

namespace MISA.AMIS.Controllers
{
    /// <summary>
    /// Api danh mục chức năng, vị trí
    /// CreatedBy: VVAn(22/04/2021)
    /// </summary>
    public class PositionsController : BaseEntityController<Position>
    {
        IPositionService _positionService;

        public PositionsController(IPositionService positionService): base(positionService)
        {
            this._positionService = positionService;
        }

        [HttpGet("child")]
        [Authorize]
        public IActionResult GetListCurrentPositions()
        {
            string id = base.GetPositionId();
            return Ok(_positionService.GetPositionById(Guid.Parse(id)));
        }
    }
}
