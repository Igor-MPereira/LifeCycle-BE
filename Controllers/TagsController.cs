using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_LifeCycle.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Controllers
{
    [ApiController]
    public class TagsController : _BaseLifeCycleController
    {
        private readonly ITagService tagService;
        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet("ToList")]
        [Authorize]

        public IActionResult ToList()
        {
            return Ok(tagService.ToList());
        }
    }
}
