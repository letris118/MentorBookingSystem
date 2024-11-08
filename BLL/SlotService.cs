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
                .Include(s => s.Student)
                .Include(s => s.Mentor)
                .ToList();
        }

        public void ShiftToAvailable(Slot slot)
        {
            _unitOfWork.GetRepository<Slot>().Insert(slot);
            _unitOfWork.Save();
        }

        public Slot? GetSelectedSlot(DateOnly date, int duration, int mentorId)
        {
            return _unitOfWork.GetRepository<Slot>().Entities
                .Where(s => s.Date == date && s.Duration == duration && s.MentorId == mentorId)
                .FirstOrDefault();
        }

        public void ShiftToNon(Slot slot)
        {
            _unitOfWork.GetRepository<Slot>().Delete(slot);
            _unitOfWork.Save();
        }

        public void ShiftToSuccess(DateOnly date, int duration, int mentorId, User student)
        {
            Slot? slot = _unitOfWork.GetRepository<Slot>().Entities
                .Where(s => s.Date == date && s.Duration == duration && s.MentorId == mentorId)
                .FirstOrDefault();

            student!.Wallet -= 50000;
            slot!.Status = 2;
            slot!.StudentId = student.Id;
            _unitOfWork.Save();


        }
    }
}
