using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class VoyageService
    {
        private readonly WebApplication1Context _context;

        public VoyageService(WebApplication1Context context)
        {
            _context = context;
        }

        //1) on vérifie si un voyage est Vide avant de le supprimer
        public async Task<bool> EffacerVoyageSiVide(int voyageId)
        {
            var voyage = await _context.Voyages.Include(v => v.UserVoyages)
                                              .SingleOrDefaultAsync(v => v.Id == voyageId);

            if(voyage == null)
            {
                throw new ArgumentException("Il n'y a aucun voyage correspondant");
            }

            if (voyage.UserVoyages.Any())
            {
                return false;
            }

            _context.Voyages.Remove(voyage);
            await _context.SaveChangesAsync();

            return true;
        }

        //Pour obtenir la liste de tous les voyages
        public async Task<IEnumerable<Voyage>> GetAll()
        {
            return await _context.Voyages.ToListAsync();
        }
    }
}
