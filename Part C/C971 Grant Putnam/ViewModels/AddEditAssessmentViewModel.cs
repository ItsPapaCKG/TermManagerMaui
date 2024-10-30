using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("Assessments","Assessments")]
    [QueryProperty("CourseId","CourseId")]
    public partial class AddEditAssessmentViewModel : IQueryAttributable
    {
        [ObservableProperty]
        private bool includeObjectiveAssessment;

        [ObservableProperty]
        private bool includePerformanceAssessment;

        [ObservableProperty]
        private string objectiveAssessmentName;

        [ObservableProperty]
        private DateTime objectiveAssessmentStart;


        [ObservableProperty]
        private DateTime objectiveAssessmentEnd;

        [ObservableProperty]
        private bool objectiveAssessmentNotify;

        [ObservableProperty]
        private string performanceAssessmentName;

        [ObservableProperty]
        private DateTime performanceAssessmentStart;


        [ObservableProperty]
        private DateTime performanceAssessmentEnd;

        [ObservableProperty]
        private bool performanceAssessmentNotify;

        [ObservableProperty]
        private ObservableCollection<Assessment> assessments;

        [ObservableProperty]
        private int courseId;

        private DatabaseService database;
        private MainViewModel mainview;

        public AddEditAssessmentViewModel(DatabaseService db, MainViewModel mvm)
        {
            database = db;
            mainview = mvm;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // If no assessments are passed through a query, create dummy assessments and uncheck both assessments
            if (!query.ContainsKey("Assessments"))
            {
                IncludeObjectiveAssessment = false;
                IncludePerformanceAssessment = false;
                return;
            }

            // If any assessments are passed, categorize

            var aS = (ObservableCollection<Assessment>)query["Assessments"];
            var receivedObjective = aS.FirstOrDefault(x => x.Type == "OA", null);
            var receivedPerformance = aS.FirstOrDefault(x => x.Type == "PA", null);

            if (receivedObjective is not null)
            {
                IncludeObjectiveAssessment = true;
                ObjectiveAssessmentName = receivedObjective.Name;
                ObjectiveAssessmentStart = receivedObjective.Start;
                ObjectiveAssessmentEnd = receivedObjective.End;
                ObjectiveAssessmentNotify = receivedObjective.Notify;
            }
            
            if (receivedPerformance is not null)
            {
                IncludePerformanceAssessment = true;
                PerformanceAssessmentName = receivedPerformance.Name;
                PerformanceAssessmentStart = receivedPerformance.Start;
                PerformanceAssessmentEnd = receivedPerformance.End;
                PerformanceAssessmentNotify = receivedPerformance.Notify;
            }
        }

        private Assessment CreateObj()
        {
            var a = new Assessment();
            a.Name = ObjectiveAssessmentName;
            a.Start = ObjectiveAssessmentStart;
            a.End = ObjectiveAssessmentEnd;
            a.Notify = ObjectiveAssessmentNotify;
            a.Type = "OA";
            a.CourseId = CourseId;

            return a;
        }

        private Assessment CreatePer()
        {
            var a = new Assessment();
            a.Name = PerformanceAssessmentName;
            a.Start = PerformanceAssessmentStart;
            a.End = PerformanceAssessmentEnd;
            a.Notify = PerformanceAssessmentNotify;
            a.Type = "PA";
            a.CourseId = CourseId;

            return a;
        }
        public async void RemoveAssessment(Assessment a)
        {
            try
            {
                await database.RemoveAssessment(a).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async void AddAssessment(Assessment a)
        {
            try
            {
                await database.AddAssessment(a).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async void UpdateAssessment(Assessment a)
        {
            try
            {
                await database.UpdateAssessment(a.Id, a).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        async Task SaveAssessments()
        {
            var oldObj = Assessments.FirstOrDefault(x => x.Type == "OA", null);
            var oldPer = Assessments.FirstOrDefault(x => x.Type == "PA", null);

            var obj = oldObj;
            var per = oldPer;

            var updatedAssessments = new ObservableCollection<Assessment>();
            // sending any operations to be run as a single operation for performance, collect all objects that will be added here
            var transactionQueue = new Dictionary<string, List<Assessment>>();
            transactionQueue.Add("Update", new List<Assessment>());
            transactionQueue.Add("Add", new List<Assessment>());
            transactionQueue.Add("Remove", new List<Assessment>());


            if (IncludeObjectiveAssessment)
            {
                obj = CreateObj();
                obj.Id = oldObj is null ? 0 : oldObj.Id;

                // to break up if statements, set "queue" as an action corresponding to adding to the "add" or "update" queue
                // in doing so, we don't need to repetitively check if values are null
                Action queue = oldObj is null ? () => transactionQueue["Add"].Add(obj) : () => transactionQueue["Update"].Add(obj);
                queue();

                updatedAssessments.Add(obj);

                goto PerformanceAssessmentCheck;
            }

            if (oldObj is Assessment a)
            {
                transactionQueue["Remove"].Add(a);
            }

            PerformanceAssessmentCheck:

                if (IncludePerformanceAssessment)
                {
                    per = CreatePer();
                    per.Id = oldPer is null ? 0 : oldPer.Id;

                    Action queue = oldPer is null ? () => transactionQueue["Add"].Add(per) : () => transactionQueue["Update"].Add(per);
                    queue();

                    updatedAssessments.Add(per);

                    goto Transactions;
            } else if (oldPer is not null)
                {
                    // if PA already exists and user excludes it, queue to remove
                    transactionQueue["Remove"].Add(oldPer);
                }

            //TODO iterate over queue to run db operations
            Transactions:
            {
                foreach (var assessment in transactionQueue["Add"])
                {
                    if (assessment is null) continue;
                    AddAssessment(assessment);
                }

                foreach (var assessment in transactionQueue["Update"])
                {
                    if (assessment is null) continue;
                    UpdateAssessment(assessment);
                }

                foreach (var assessment in transactionQueue["Remove"])
                {
                    if (assessment is null) continue;
                    RemoveAssessment(assessment);
                }

                mainview.RefreshCourses();

                await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
                {
                    {"Assessments", updatedAssessments }
                });

                
            }
        }
    }
}
