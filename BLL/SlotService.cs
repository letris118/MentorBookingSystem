using DAL.Entities;
using DAL.UnitOfWork;

namespace BLL
{
    public class SlotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Slot> GetSlotsByDate(DateOnly date)
        {
            return _unitOfWork.GetRepository<Slot>()
                .Entities
                .Where(s => s.Date == date)
                .ToList();
        }

    }
        
    }

