using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Kotlin.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // If no assessments are passed through a query, create dummy assessments and uncheck both assessments
            if (!query.ContainsKey("Assessments"))
            {
                Assessment[] a = new Assessment[2];

                a[0] = new Assessment { Type = "OA" };
                a[1] = new Assessment { Type = "PA" };

                Assessments = a;

                IncludeObjectiveAssessment = false;
                IncludePerformanceAssessment = false;
                return;
            }

            var a = (Assessment[])query["Assessments"];
            if (a[0] is not null)
            {
                ObjectiveAssessmentName = a[0].Name;
                ObjectiveAssessmentStart = a[0].Start;
            }

            IncludeObjectiveAssessment = a[0] is not null;
            IncludePerformanceAssessment = a[1] is not null;
        }
    }
}
