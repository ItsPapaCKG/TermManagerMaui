using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("SelectedTerm","SelectedTerm")]
    [QueryProperty("EditMode","EditMode")]
    public partial class AddEditTermViewModel : IQueryAttributable
    {
        [ObservableProperty]
        private Term selectedTerm;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitTermChangesCommand))]
        private string selectedTermName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitTermChangesCommand))]
        private DateTime selectedStartDate;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitTermChangesCommand))]
        private DateTime selectedEndDate;

        [ObservableProperty]
        private bool editMode;

        [ObservableProperty]
        private bool selectedNotify;

        private DatabaseService database;
        private MainViewModel mainview;
        private WeakReferenceMessenger _messenger;

        public AddEditTermViewModel(DatabaseService db, MainViewModel mvm)
        {
            database = db;
            mainview = mvm;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("EditMode"))
                EditMode = bool.Parse(query["EditMode"].ToString() ?? "false");

            if (query.ContainsKey("SelectedTerm"))
                SelectedTerm = query["SelectedTerm"] as Term ?? new Term();

            if (SelectedTerm is null)
            {
                SelectedTerm = new Term();
            }
            // Now we can check the EditMode and set additional properties
            if (EditMode == true)
            {
                SelectedTermName = SelectedTerm.Name;
                SelectedStartDate = SelectedTerm.Start;
                SelectedEndDate = SelectedTerm.End;
                SelectedNotify = SelectedTerm.Notify;
            }
            else
            {
                SelectedStartDate = DateTime.Today;
                SelectedEndDate = DateTime.Today.AddDays(1);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSaveTermChanges))]
        async Task SubmitTermChangesAsync(Term term)
        {
            var t = new Term { Name = SelectedTermName, Start = SelectedStartDate, End = SelectedEndDate, Notify = SelectedNotify };

            if (EditMode == true)
            {
                await database.UpdateTerm(term.Id, SelectedTermName, SelectedStartDate, SelectedEndDate, SelectedNotify);
                t.Id = term.Id;
            } else
            {
                await database.AddTerm(t).ConfigureAwait(false);
            }

            WeakReferenceMessenger.Default.Send(new UpdateTermMessage(t));

            await MainThread.InvokeOnMainThreadAsync(async () => { 
                await Shell.Current.GoToAsync("..", true);
            });

            
        }

        public bool CanSaveTermChanges()
        {
            return !string.IsNullOrWhiteSpace(SelectedTermName) && SelectedStartDate < SelectedEndDate;
        }

    }
}
