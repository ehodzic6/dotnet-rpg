using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
         private static List<Character> characters= new List<Character>{
            new Character(),
            new Character {Id=1, Name = "Sam"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto NewCharacter)
        {
            var serviceResponce = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>( NewCharacter);
            character.Id = characters.Max(c => c.Id) +1;  
            characters.Add(character);
            serviceResponce.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
           

            return serviceResponce;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

              try{
            Character character = characters.First(c => c.Id == id);

            characters.Remove(character);

            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c) ).ToList();
            }
            catch(Exception e){
                response.Success= false;
                response.Message= e.Message;

            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>> { Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()};
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id){
            
            var serviceResponse = new ServiceResponse<GetCharacterDto>(); 
            var character=characters.FirstOrDefault(c => c.Id == id); 
            serviceResponse.Data= _mapper.Map<GetCharacterDto>(character);
            return  serviceResponse;
            
                }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
          
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

              try{
            Character character = characters.FirstOrDefault(c => c.Id == updateCharacterDto.Id);

            character.HitPoints = updateCharacterDto.HitPoints;
            character.Defense = updateCharacterDto.Defense;
            character.Intelligence= updateCharacterDto.Intelligence;
            character.Strength= updateCharacterDto.Strength;
            character.Class = updateCharacterDto.Class;
            character.Name = updateCharacterDto.Name;
            //_mapper.Map(updateCharacterDto, character);
            response.Data= _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception e){
                response.Success= false;
                response.Message= e.Message;

            }
            return response;
        }
    }
}