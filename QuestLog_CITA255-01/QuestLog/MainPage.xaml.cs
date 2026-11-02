// =============================================================================
// TODO 1
//
// Add the using line for ObservableCollection, right here at the very top:
////
// Without it you get "The type or namespace name 'ObservableCollection' could
// not be found". Everybody hits this one. It is the easiest thing on the page
// to fix and the easiest thing to forget.
// =============================================================================
using System.Collections.ObjectModel;

namespace QuestLog;

public partial class MainPage : ContentPage
{
    // -------------------------------------------------------------------------
    // TODO 2
    //
    // Right now this is a plain List. A plain List cannot tell anybody it
    // changed -- which is why there is a Refresh() method further down doing
    // the ItemsSource dance by hand.
    //
    // Replace the line below with a PROPERTY holding an ObservableCollection:
    //
    // Three things worth noticing:
    //   * It is a PROPERTY (capital), not a variable. Binding can only see
    //     properties -- that is what last Thursday was about.
    //   * get only, no set. The collection is created once, right here.
    //   * = new() creates it. Never assign a new one later, or the screen stops
    //     watching the one it was given.
    //
    // You will also need to change every "quests" below to "Quests".
    // -------------------------------------------------------------------------
    ObservableCollection<string> Quests => new ObservableCollection<string>();

    // The quests the button hands out, in order. Nothing to change here.
    string[] pool =
    {
        "Clear the cellar rats",
        "Escort the merchant to Dunmoor",
        "Find the alchemist's cat",
        "Light the three beacons",
        "Return the sword to the lake",
        "Investigate the noise under the inn"
    };

    int next = 0;

    public MainPage()
    {
        InitializeComponent();
        
        Quests.Add("Speak to the innkeeper");
        Quests.Add("Buy a lantern");

        // ---------------------------------------------------------------------
        // TODO 3
        //
        // Add this line, below InitializeComponent():
        //
        //
        // It means "when the XAML says {Binding something}, look for it on THIS
        // page object." Leave it out and every binding on the page quietly
        // finds nothing -- no error, just an empty list.
        // ---------------------------------------------------------------------

        Refresh();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        Quests.Add(pool[next]);

        next++;
        if (next >= pool.Length)
            next = 0;

        // ---------------------------------------------------------------------
        // TODO 5  (do this one LAST, after 1-4 are working)
        //
        // Delete the Refresh() call below. Then delete the Refresh() method
        // entirely. The list should still update when you press the button --
        // that is the whole point of today.
        //
        // One line does NOT come for free: the count label. A collection
        // announces its own changes; a Label does not. So update the CountLabel text over here
        //
        // Binding is not magic glue that updates everything on the page. It
        // updates what you bound. The label is still your job.
        // ---------------------------------------------------------------------
        Refresh();
    }

    // -------------------------------------------------------------------------
    // This whole method exists only because a plain List cannot announce
    // changes. Setting ItemsSource to null and then back is how you force a
    // CollectionView to look again.
    //
    // By the end of the lab this method should not exist.
    // -------------------------------------------------------------------------
    void Refresh()
    {
        QuestView.ItemsSource = null;
        QuestView.ItemsSource = Quests;

        CountLabel.Text = $"{Quests.Count} quests";
    }
}
