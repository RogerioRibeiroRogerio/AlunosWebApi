using AlunosApi.Models;
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
    //[Produces("application/json)")]
    public class AlunosController : ControllerBase
    {
        //este controlador foi criado com add controloller api empty inicialmente

        private InterfaceAlunoService _alunoService;

        public AlunosController(InterfaceAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)] recurso possivel
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter alunos");

            }

        }
        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>>
            GetAlunosbyName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);
                if (alunos == null)
                    return NotFound($"Não existem com o critério {nome}");

                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request inválido");

            }
        }
        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null)
                    return NotFound($"Não existem o aluno com id={id}");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request da inválido");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch
            {
                return BadRequest("Request da tanga");
            }
        }
        [HttpPut("{id:int}")] //o resultado tem de ser o mesmo, mesmo no caso de varias solicitações
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    //return NoContent();
                    return Ok($"Aluno com id={id} foi alterado e atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados Inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request tangoso");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno com id={id} foi apagado com sucesso ");
                }
                else
                {
                    return NotFound($"Aluno com o id={id} não encontrado");
                }
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }
    }
}

