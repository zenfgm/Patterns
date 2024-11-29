using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManage
{
    interface IRequestState
    {
        void SendRequest();
        void Pay();
        void Confirm();
        void Cancel();
    }

    class RequestContext
    {
        public IRequestState State { get; set; }

        public RequestContext() => State = new CreatedState(this);
    }

    class CreatedState : IRequestState
    {
        private readonly RequestContext _context;

        public CreatedState(RequestContext context) => _context = context;

        public void SendRequest()
        {
            Console.WriteLine("Request sent. Moving to WaitingForPayment state.");
            _context.State = new WaitingForPaymentState(_context);
        }

        public void Pay() => Console.WriteLine("Cannot pay. Request not sent.");
        public void Confirm() => Console.WriteLine("Cannot confirm. Request not paid.");
        public void Cancel() => Console.WriteLine("Cannot cancel. Request not sent.");
    }

    class WaitingForPaymentState : IRequestState
    {
        private readonly RequestContext _context;

        public WaitingForPaymentState(RequestContext context) => _context = context;

        public void SendRequest() => Console.WriteLine("Request already sent.");
        public void Pay()
        {
            Console.WriteLine("Payment received. Moving to Paid state.");
            _context.State = new PaidState(_context);
        }

        public void Confirm() => Console.WriteLine("Cannot confirm. Request not paid.");
        public void Cancel()
        {
            Console.WriteLine("Request cancelled. Moving to Cancelled state.");
            _context.State = new CancelledState();
        }
    }

    class PaidState : IRequestState
    {
        private readonly RequestContext _context;

        public PaidState(RequestContext context) => _context = context;

        public void SendRequest() => Console.WriteLine("Cannot send request. Already paid.");
        public void Pay() => Console.WriteLine("Request already paid.");
        public void Confirm()
        {
            Console.WriteLine("Request confirmed. Moving to Confirmed state.");
            _context.State = new ConfirmedState();
        }

        public void Cancel() => Console.WriteLine("Cannot cancel. Request already paid.");
    }

    class ConfirmedState : IRequestState
    {
        public void SendRequest() => Console.WriteLine("Request already completed.");
        public void Pay() => Console.WriteLine("Request already completed.");
        public void Confirm() => Console.WriteLine("Request already confirmed.");
        public void Cancel() => Console.WriteLine("Cannot cancel. Request completed.");
    }

    class CancelledState : IRequestState
    {
        public void SendRequest() => Console.WriteLine("Request cancelled.");
        public void Pay() => Console.WriteLine("Request cancelled.");
        public void Confirm() => Console.WriteLine("Request cancelled.");
        public void Cancel() => Console.WriteLine("Request already cancelled.");
    }

    class Program
    {
        static void Main()
        {
            var request = new RequestContext();

            request.State.SendRequest();
            request.State.Pay();
            request.State.Confirm();
            request.State.Cancel();
        }
    }
}
