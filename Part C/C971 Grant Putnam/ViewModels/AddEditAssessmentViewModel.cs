using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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
        private Assessment[] assessments;

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

            var aS = (Assessment[])query["Assessments"];
            if (aS[0] is not null)
            {
                IncludeObjectiveAssessment = true;
                ObjectiveAssessmentName = aS[0].Name;
                ObjectiveAssessmentStart = aS[0].Start;
                ObjectiveAssessmentEnd = aS[0].End;
                ObjectiveAssessmentNotify = aS[0].Notify;
            }
            
            if (aS[1] is not null)
            {
                IncludePerformanceAssessment = true;
                PerformanceAssessmentName = aS[0].Name;
                PerformanceAssessmentStart = aS[0].Start;
                PerformanceAssessmentEnd = aS[0].End;
                PerformanceAssessmentNotify = aS[0].Notify;
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

            return a;
        }
        public async void RemoveAssessment(Assessment a)
        {
            try
            {
                await database.RemoveAssessment(a.Id).ConfigureAwait(false);
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
            var obj = Assessments[0] ?? new Assessment();
            var per = Assessments[1] ?? new Assessment();

            // sending any operations to be run as a single operation for performance, collect all objects that will be added here
            var transactionQueue = new Dictionary<string, Assessment[]>();

            if (IncludeObjectiveAssessment)
            {
                obj.Id = Assessments[0] is null ? 0 : Assessments[0].Id;
                obj.Name = ObjectiveAssessmentName;
                obj.Start = ObjectiveAssessmentStart;
                obj.End = ObjectiveAssessmentEnd;
                obj.Notify = ObjectiveAssessmentNotify;
                obj.Type = "OA";
                obj.CourseId = CourseId;

                // to break up if statements, set "queue" as an action corresponding to adding to the "add" or "update" queue
                // in doing so, we don't need to repetitively check if values are null
                Action queue = Assessments[0] is null ? () => transactionQueue["Add"].Append(obj) : () => transactionQueue["Update"].Append(obj);
                queue();
                goto PerformanceAssessmentCheck;
            }

            if (Assessments[0] is Assessment a)
            {
                transactionQueue["Remove"].Append(a);
            }

            PerformanceAssessmentCheck:

                if (IncludePerformanceAssessment)
                {
                    per.Id = Assessments[1] is null ? 0 : Assessments[1].Id;
                    per.Name = PerformanceAssessmentName;
                    per.Start = PerformanceAssessmentStart;
                    per.End = PerformanceAssessmentEnd;
                    per.Notify = PerformanceAssessmentNotify;
                    per.Type = "PA";
                    per.CourseId = CourseId;

                    Action queue = Assessments[1] is null ? () => transactionQueue["Add"].Append(per) : () => transactionQueue["Update"].Append(per);
                    queue(); 
                    //goto Transactions;
            } else if (Assessments[1] is not null)
                {
                    // if PA already exists and user excludes it, queue to remove
                    transactionQueue["Remove"].Append(Assessments[1]);
                }

            //TODO iterate over queue to run db operations

        }
    }
}
