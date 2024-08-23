using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

using System.Threading.Tasks;

public class GameHistory
{
    private const string FileName = "gameHistory.json";

    public ObservableCollection<Results> GameRecords { get; private set; }

    public GameHistory()
    {
        GameRecords = new ObservableCollection<Results>();
    }

    public void AddGameRecord(Results record)
    {
        GameRecords.Add(record);
    }

    public async Task SaveGameHistoryAsync()
    {
        var json = JsonConvert.SerializeObject(GameRecords); 
        var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task LoadGameHistoryAsync()
    {
        Debug.WriteLine("LoadGameHistoryAsync started.");

        try
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
            if (File.Exists(path))
            {
                var json = await File.ReadAllTextAsync(path);
                GameRecords = JsonConvert.DeserializeObject<ObservableCollection<Results>>(json)
                               ?? new ObservableCollection<Results>();

                Debug.WriteLine("LoadGameHistoryAsync completed successfully.");
            }
            else
            {
                Debug.WriteLine("No game history file found.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"LoadGameHistoryAsync error: {ex.Message}");
            Debug.WriteLine(ex.StackTrace);
        }
    }

}
