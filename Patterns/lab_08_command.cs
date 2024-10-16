using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class LightOnCommand : ICommand
    {
        private Light _light;

        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.On();
        }

        public void Undo()
        {
            _light.Off();
        }
    }
    public class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.Off();
        }

        public void Undo()
        {
            _light.On();
        }
    }
    public class Light
    {
        public void On()
        {
            Console.WriteLine("Свет включен.");
        }

        public void Off()
        {
            Console.WriteLine("Свет выключен.");
        }
    }
    public class Television
    {
        public void On()
        {
            Console.WriteLine("Телевизор включен.");
        }
        public void Off()
        {
            Console.WriteLine("Телевизор выключен.");
        }
    }
    public class TelevisionCommandOn : ICommand
    {
        private Television television;
        public TelevisionCommandOn()
        {
            television = new Television();
        }

        public void Execute()
        {
            television.On();
        }

        public void Undo()
        {
            television.Off();
        }
    }
    public class TelevisionCommandOff : ICommand
    {
        private Television television;
        public TelevisionCommandOff()
        {
            television = new Television();
        }

        public void Execute()
        {
            television.Off();
        }

        public void Undo()
        {
            television.On();
        }
    }
    public class RemoteControl
    {
        private ICommand _onCommand;
        private ICommand _offCommand;

        public void SetCommands(ICommand onCommand, ICommand offCommand)
        {
            _onCommand = onCommand;
            _offCommand = offCommand;
        }

        public void PressOnButton()
        {
            _onCommand.Execute();
        }

        public void PressOffButton()
        {
            _offCommand.Execute();
        }

        public void PressUndoButton()
        {
            _onCommand.Undo();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Light livingRoomLight = new Light();
            Television tv = new Television();

            ICommand lightOn = new LightOnCommand(livingRoomLight);
            ICommand lightOff = new LightOffCommand(livingRoomLight); 

            RemoteControl remote = new RemoteControl();

            remote.SetCommands(lightOn, lightOff);
            Console.WriteLine("Управление светом:");
            remote.PressOnButton();
            remote.PressOffButton();
            remote.PressUndoButton();

            remote.SetCommands(new TelevisionCommandOn(), new TelevisionCommandOff());
            Console.WriteLine("\nУправление телевизором:");
            remote.PressOnButton();
            remote.PressOffButton();
        }

    }
}
