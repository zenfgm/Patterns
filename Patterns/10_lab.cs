using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    //FACADE
    public class AudioSystem
    {
        public void TurnOn()
        {
            Console.WriteLine("Audio system is turned on.");
        }
        public void SetVolume(int level)
        {
            Console.WriteLine($"Audio volume is set to {level}.");
        }
        public void TurnOff()
        {
            Console.WriteLine("Audio system is turned off.");
        }
    }
    public class VideoProjector
    {
        public void TurnOn()
        {
            Console.WriteLine("Video projector is turned on.");
        }
        public void SetResolution(string resolution)
        {
            Console.WriteLine($"Video resolution is set to {resolution}.");
        }
        public void TurnOff()
        {
            Console.WriteLine("Video projector is turned off.");
        }
    }
    public class LightingSystem
    {
        public void TurnOn()
        {
            Console.WriteLine("Lights are turned on.");
        }
        public void SetBrightness(int level)
        {
            Console.WriteLine($"Lights brightness is set to {level}.");
        }
        public void TurnOff()
        {
            Console.WriteLine("Lights are turned off.");
        }
    }
    
    public class HomeTheaterFacade
    {
        private AudioSystem _audioSystem;
        private VideoProjector _videoProjector;
        private LightingSystem _lightingSystem;

        public HomeTheaterFacade(AudioSystem audioSystem, VideoProjector videoProjector,
            LightingSystem lightingSystem)
        {
            _audioSystem = audioSystem;
            _videoProjector = videoProjector;
            _lightingSystem = lightingSystem;
        }

        public void StartMovie()
        {
            Console.WriteLine("Preparing to start the movie...");
            _lightingSystem.TurnOn();
            _lightingSystem.SetBrightness(5);
            _audioSystem.TurnOn();
            _audioSystem.SetVolume(15);
            _videoProjector.TurnOn();
            _videoProjector.SetResolution("UHD 4K");
            Console.WriteLine("all done. Movie Started. Columbia Pictures present...");
        }

        public void EndMovie()
        {
            Console.WriteLine("Shutting down the movie...");
            _videoProjector.TurnOff();
            _audioSystem.TurnOff();
            _lightingSystem.TurnOff();
            Console.WriteLine("Movie ended");
        }
    }
    //Component
    public abstract class FileSystemComponent
    {
        protected string _name;

        public FileSystemComponent(string name)
        {
            _name = name;
        }
        public abstract void Display(int depth);
        public virtual void Add(FileSystemComponent component)
        {
            throw new NotImplementedException();
        }
        public virtual void Remove(FileSystemComponent component)
        {
            throw new NotImplementedException();
        }

        public virtual FileSystemComponent GetChild(int index)
        {
            throw new NotImplementedException();
        }
    }
    public class File : FileSystemComponent
    {
        public File(string name) : base(name) { }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + "File: " + _name);
        }
    }
    public class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> _children = new List<FileSystemComponent>();
        public Directory(string name) : base(name) { }
        public override void Add(FileSystemComponent component)
        {
            _children.Add(component);
        }
        public override void Remove(FileSystemComponent component)
        {
            _children.Remove(component);
        }
        public override FileSystemComponent GetChild(int index)
        {
            return _children[index];
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + " Directory: " + _name);
            foreach (var component in _children)
            {
                component.Display(depth + 2);
            }
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            //FACADE
            AudioSystem audio = new AudioSystem();
            VideoProjector video = new VideoProjector();
            LightingSystem light = new LightingSystem();

            HomeTheaterFacade homeTheater = new HomeTheaterFacade(audio, video, light);

            homeTheater.StartMovie();
            Console.WriteLine();
            homeTheater.EndMovie();
            Console.WriteLine();

            //Component
            Directory root = new Directory("Root");
            File file1 = new File("First_file.txt");
            File file2 = new File("Second_file.txt");

            Directory subDir = new Directory("SubDirectory");
            File subFile1 = new File("SubFile1.txt");

            root.Add(file1);
            root.Add(file2);
            subDir.Add(subFile1);
            root.Add(subDir);

            root.Display(1);

        }
    }
}
