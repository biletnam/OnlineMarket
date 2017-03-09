using AutoMapper;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    public class ArchiveController : ApiController
    {
        private IDealService _dealService;

        public ArchiveController(IDealService dealService)
        {
            _dealService = dealService;
        }

        //[Authorize]
        [HttpGet]
        public IList<ArchiveViewModel> GetArchive(string email)
        {
            var deals = _dealService.GetDealsByUser(email);
            return Mapper.Map<IList<ArchiveViewModel>>(deals);
        }
    }
}
