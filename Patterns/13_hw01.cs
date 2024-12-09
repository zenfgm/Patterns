using System;
using System.Collections.Generic;

namespace RecruitmentProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            RecruitmentProcess recruitment = new RecruitmentProcess();
            recruitment.StartProcess();
        }
    }

    public class RecruitmentProcess
    {
        public void StartProcess()
        {
            Console.WriteLine("Начало процесса найма сотрудников...");

            if (!CreateAndApproveRequest())
            {
                Console.WriteLine("Процесс завершен: заявка отклонена.");
                return;
            }

            List<Candidate> candidates = PublishAndCollectApplications();
            List<Candidate> shortlistedCandidates = FilterApplications(candidates);

            foreach (var candidate in shortlistedCandidates)
            {
                Console.WriteLine($"Начало собеседования с кандидатом: {candidate.Name}");
                if (ConductInterviews(candidate))
                {
                    Console.WriteLine($"Кандидату {candidate.Name} сделано предложение!");
                    if (!ProcessOffer(candidate))
                    {
                        Console.WriteLine($"Кандидат {candidate.Name} отклонил предложение.");
                    }
                }
                else
                {
                    Console.WriteLine($"Кандидат {candidate.Name} не прошел собеседование.");
                }
            }

            Console.WriteLine("Процесс найма завершен.");
        }

        private bool CreateAndApproveRequest()
        {
            Console.WriteLine("Руководитель создает заявку...");
            bool requestApproved = new HRDepartment().ApproveRequest();

            if (!requestApproved)
            {
                Console.WriteLine("Заявка отклонена, требуется доработка.");
                return false;
            }

            Console.WriteLine("Заявка успешно утверждена.");
            return true;
        }

        private List<Candidate> PublishAndCollectApplications()
        {
            Console.WriteLine("Вакансия опубликована...");
            List<Candidate> candidates = new List<Candidate>
            {
                new Candidate { Name = "Иван Иванов", ResumeQuality = 80 },
                new Candidate { Name = "Анна Петрова", ResumeQuality = 60 },
                new Candidate { Name = "Алексей Смирнов", ResumeQuality = 40 }
            };
            Console.WriteLine("Заявки от кандидатов собраны.");
            return candidates;
        }

        private List<Candidate> FilterApplications(List<Candidate> candidates)
        {
            Console.WriteLine("HR проверяет анкеты кандидатов...");
            List<Candidate> shortlisted = new List<Candidate>();

            foreach (var candidate in candidates)
            {
                if (candidate.ResumeQuality >= 50)
                {
                    Console.WriteLine($"Кандидат {candidate.Name} подходит.");
                    shortlisted.Add(candidate);
                }
                else
                {
                    Console.WriteLine($"Кандидат {candidate.Name} отклонен.");
                }
            }

            return shortlisted;
        }

        private bool ConductInterviews(Candidate candidate)
        {
            Console.WriteLine($"Первичное интервью для {candidate.Name}...");
            bool hrResult = new HRDepartment().ConductHRInterview();

            Console.WriteLine($"Техническое интервью для {candidate.Name}...");
            bool techResult = new DepartmentManager().ConductTechnicalInterview();

            return hrResult && techResult;
        }

        private bool ProcessOffer(Candidate candidate)
        {
            Console.WriteLine($"Кандидат {candidate.Name} получил оффер.");
            bool accepted = candidate.ConfirmOffer();

            if (accepted)
            {
                Console.WriteLine($"Кандидат {candidate.Name} принял оффер.");
                AddEmployeeToDatabase(candidate);
                NotifyITDepartment();
            }

            return accepted;
        }

        private void AddEmployeeToDatabase(Candidate candidate)
        {
            Console.WriteLine($"Добавление сотрудника {candidate.Name} в базу данных...");
        }

        private void NotifyITDepartment()
        {
            Console.WriteLine("Уведомление IT-отдела о настройке рабочего места...");
        }
    }

    public class HRDepartment
    {
        public bool ApproveRequest()
        {
            Console.WriteLine("HR проверяет заявку...");
            return true;
        }

        public bool ConductHRInterview()
        {
            Console.WriteLine("HR проводит интервью...");
            return true;
        }
    }

    public class DepartmentManager
    {
        public bool ConductTechnicalInterview()
        {
            Console.WriteLine("Руководитель проводит техническое собеседование...");
            return true;
        }
    }

    public class Candidate
    {
        public string Name { get; set; }
        public int ResumeQuality { get; set; }

        public bool ConfirmOffer()
        {
            Console.WriteLine($"{Name} принимает оффер...");
            return true;
        }
    }
}
