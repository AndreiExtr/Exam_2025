using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project;

public partial class AdministratorPage : Window, INotifyPropertyChanged
{
    public User CurrenUser {  get; set; }
    public List<User> Users { get; set; }
    public AdministratorPage(User user)
    {
        InitializeComponent();
        CurrenUser = user;
        DataContext = this;
        LoadUsers();

    }

    public async void LoadUsers()
    {
        using (var db = new UserItContext())
        {
            Users = await db.Users
                .Include(u => u.Role)
                .ToListAsync();

            OnPropertyChanged(nameof(Users));
        }
    }

    private async void AddNewOnClick(object sender, RoutedEventArgs e)
    {
        var newUser = new NewUserPopup();
        await newUser.ShowDialog(this);
        LoadUsers();
    }

    private async void ComboboxSelection(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox { DataContext: User user})
        {
            using (var db = new UserItContext())
            {
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
        }
    }

    private void ExitOnClick(object sender, RoutedEventArgs e)
    {
        new AuthorizationPage().Show();
        Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string args)
    {
        PropertyChanged?.Invoke(this, new(args));
    }
}