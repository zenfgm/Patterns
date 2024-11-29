using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    public interface IState
    {
        void SelectTicket();
        void InsertMoney(decimal amount);
        void DispenseTicket();
        void CancelTransaction();
    }
    public class IdleState : IState
    {
        private readonly TicketMachine _machine;

        public IdleState(TicketMachine machine) => _machine = machine;

        public void SelectTicket()
        {
            Console.WriteLine("Ticket selected. Waiting for money...");
            _machine.SetState(_machine.WaitingForMoneyState);
        }

        public void InsertMoney(decimal amount) => Console.WriteLine("Please select a ticket first.");
        public void DispenseTicket() => Console.WriteLine("No ticket selected.");
        public void CancelTransaction() => Console.WriteLine("No transaction to cancel.");
    }

    public class WaitingForMoneyState : IState
    {
        private readonly TicketMachine _machine;

        public WaitingForMoneyState(TicketMachine machine) => _machine = machine;

        public void SelectTicket() => Console.WriteLine("Ticket already selected. Please insert money.");
        public void InsertMoney(decimal amount)
        {
            Console.WriteLine($"Money received: {amount:C}");
            _machine.SetState(_machine.MoneyReceivedState);
        }

        public void DispenseTicket() => Console.WriteLine("Insert money first.");
        public void CancelTransaction()
        {
            Console.WriteLine("Transaction canceled.");
            _machine.SetState(_machine.IdleState);
        }
    }

    public class MoneyReceivedState : IState
    {
        private readonly TicketMachine _machine;

        public MoneyReceivedState(TicketMachine machine) => _machine = machine;

        public void SelectTicket() => Console.WriteLine("Ticket already selected.");
        public void InsertMoney(decimal amount) => Console.WriteLine("Money already received.");
        public void DispenseTicket()
        {
            Console.WriteLine("Dispensing ticket...");
            _machine.SetState(_machine.TicketDispensedState);
        }

        public void CancelTransaction()
        {
            Console.WriteLine("Transaction canceled. Refunding money...");
            _machine.SetState(_machine.IdleState);
        }
    }

    public class TicketDispensedState : IState
    {
        private readonly TicketMachine _machine;

        public TicketDispensedState(TicketMachine machine) => _machine = machine;

        public void SelectTicket() => Console.WriteLine("Transaction complete. Start a new one.");
        public void InsertMoney(decimal amount) => Console.WriteLine("Transaction complete. Start a new one.");
        public void DispenseTicket() => Console.WriteLine("Ticket already dispensed.");
        public void CancelTransaction() => Console.WriteLine("Transaction already complete.");
    }
    public class TicketMachine
    {
        public IState IdleState { get; }
        public IState WaitingForMoneyState { get; }
        public IState MoneyReceivedState { get; }
        public IState TicketDispensedState { get; }

        private IState _currentState;

        public TicketMachine()
        {
            IdleState = new IdleState(this);
            WaitingForMoneyState = new WaitingForMoneyState(this);
            MoneyReceivedState = new MoneyReceivedState(this);
            TicketDispensedState = new TicketDispensedState(this);

            _currentState = IdleState;
        }

        public void SetState(IState state) => _currentState = state;

        public void SelectTicket() => _currentState.SelectTicket();
        public void InsertMoney(decimal amount) => _currentState.InsertMoney(amount);
        public void DispenseTicket() => _currentState.DispenseTicket();
        public void CancelTransaction() => _currentState.CancelTransaction();
    }
    class Program
    {
        static void Main()
        {
            var machine = new TicketMachine();

            machine.SelectTicket();
            machine.InsertMoney(10);
            machine.DispenseTicket();
        }
    }

}
