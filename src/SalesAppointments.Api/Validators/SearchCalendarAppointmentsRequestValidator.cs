using SalesAppointments.Models;
using FluentValidation;

namespace SalesAppointments.Validators
{
    public class SearchCalendarAppointmentsRequestValidator : AbstractValidator<SearchCalendarAppointmentsRequest>
    {
        public SearchCalendarAppointmentsRequestValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(BeAValidDate)
                .WithMessage("Date is not in a valid date format e.g. 2024-10-29");

            RuleFor(x => x.Language)
                .NotEmpty();

            RuleFor(x => x.Products)
                .NotEmpty();

            RuleFor(x => x.Rating)
                .NotEmpty();
        }

        private bool BeAValidDate(string date) 
            => DateTime.TryParse(date, out _);
    }
}
