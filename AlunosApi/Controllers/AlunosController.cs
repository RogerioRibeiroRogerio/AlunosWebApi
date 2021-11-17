using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        //este controlador foi criado com add controloller api empty

        private InterfaceAlunoService _alunoService;

        public AlunosController(InterfaceAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
    }
}
