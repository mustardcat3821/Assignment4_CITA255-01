using System.Collections.ObjectModel;
using System.Text.Json;

namespace QuestLog;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Quest> Quests { get; } = new ObservableCollection<Quest>();

    string path = Path.Combine(FileSystem.AppDataDirectory, "quests.json");

    public MainPage()
    {
        InitializeComponent();

        BindingContext = this;

        Load();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlertAsync("Error", "Please enter a quest title.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(GiverEntry.Text))
        {
            await DisplayAlertAsync("Error", "Please enter a quest giver.", "OK");
            return;
        }

        if (!int.TryParse(RewardEntry.Text, out int reward))
        {
            await DisplayAlertAsync("Error", "Please enter a number for the reward.", "OK");
            return;
        }

        if (reward < 0)
        {
            await DisplayAlertAsync("Error", "Reward cannot be negative.", "OK");
            return;
        }

        Quest quest = new Quest
        {
            Title = TitleEntry.Text.Trim(),
            Giver = GiverEntry.Text.Trim(),
            Reward = reward
        };

        Quests.Add(quest);

        Save();

        TitleEntry.Text = "";
        GiverEntry.Text = "";
        RewardEntry.Text = "";
    }

    private async void OnRemoveClicked(object sender, EventArgs e)
    {
        if (QuestView.SelectedItem == null)
        {
            await DisplayAlertAsync("Error", "Please select a quest.", "OK");
            return;
        }

        Quest quest = (Quest)QuestView.SelectedItem;

        Quests.Remove(quest);

        Save();
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        Quests.Clear();

        Save();
    }

    void Save()
    {
        string json = JsonSerializer.Serialize(Quests);

        File.WriteAllText(path, json);
    }

    void Load()
    {
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);

        List<Quest>? saved = JsonSerializer.Deserialize<List<Quest>>(json);

        if (saved == null)
        {
            return;
        }

        foreach (Quest quest in saved)
        {
            Quests.Add(quest);
        }
    }
}