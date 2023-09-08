using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Data;


namespace ShopTARge22.ApplicationServices.Services
{
    public class SpaceshipsServices : ISpaceshipsServices
    {
        private readonly ShopTARge22Context _context;

        public SpaceshipsServices
            (
                ShopTARge22Context context
            )
        {
            _context = context;
        }


        public async Task<Spaceship> Create(SpaceshipDto dto)
        {
            Spaceship spaceship = new Spaceship();

            spaceship.Id = Guid.NewGuid();
            spaceship.Name = dto.Name;
            spaceship.Type = dto.Type;
            spaceship.BuiltDate = dto.BuiltDate;
            spaceship.Passengers = dto.Passengers;
            spaceship.CargoWeight = dto.CargoWeight;
            spaceship.Crew = dto.Crew;
            spaceship.EnginePower = dto.EnginePower;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            await _context.Spaceships.AddAsync(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }
    }
}
