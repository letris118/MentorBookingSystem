using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class SlotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Slot> GetAllSlots()
        {
            return _unitOfWork.GetRepository<Slot>().Entities
                .Where(s => s.Date >= DateOnly.FromDateTime(DateTime.Now))
                .Include(s => s.Mentor)
                .ToList();
        }
    }
}
