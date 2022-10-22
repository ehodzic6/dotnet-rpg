using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
    
       public CharacterController (ICharacterService character){   

            _characterService = character;
       

       }

        [HttpGet("GetAll")]
        
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(){

            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id ){

            return Ok( await _characterService.GetCharacterById(id));
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto NewCharacter){

            

            return Ok(await _characterService.AddCharacter(NewCharacter));
        }

        
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto NewCharacter){

            
            var response = await _characterService.UpdateCharacter(NewCharacter);

            if(response.Data == null){

                return NotFound(response);
            }

            return Ok(response);
        } 

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id ){

            var response = await _characterService.DeleteCharacter(id);

            if(response.Data == null){

                return NotFound(response);
            }

            return Ok(response);
        }
        


    }
}