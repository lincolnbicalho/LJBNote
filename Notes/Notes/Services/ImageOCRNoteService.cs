using Notes.Models;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using static OpenAI_API.Chat.ChatMessage;


namespace Notes.Services;

public class ImageOCRNoteService : IImageOCRNoteService
{
    private readonly IConfiguration _config;
    private readonly ILogger<ImageOCRNoteService> _logger;
    private readonly string? _key;
    private readonly string? _uploadPath;
    public ImageOCRNoteService(IConfiguration config, ILogger<ImageOCRNoteService> logger)
    {
        _config = config;
        _logger = logger;
        _key = _config.GetValue<string>("ImageUpload:SecretKeyApi");
        _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _config.GetValue<string>("ImageUpload:Path"));
    }
    public async Task<List<ImageNoteModel>> ReadImages(int NoteId)
    {
        _logger.LogInformation("Starting loading the files for Note {id}", NoteId);
        List<string> files = Directory.GetFiles(_uploadPath).ToList();
        List<ImageNoteModel> imagesNote = new List<ImageNoteModel>();
        var noteFiles = files.Where(w => 
                                        w.Contains($"Note{NoteId}") 
                                        && !w.Contains("Processed"))?.ToList();

        foreach (var item in noteFiles)
        {
            string text = await CallAIOcr(item);
            imagesNote.Add(new ImageNoteModel
            {
                NoteID = NoteId,
                OCRText = text,
                Path = item
            });
            RemaneFilesToProcessed(item);
            _logger.LogInformation($"{item}");
        }


        return imagesNote;
    }

    public int NumberOfImagestoProcess(int NoteId) 
    {
        _logger.LogInformation("Starting loading the files for Note {id}", NoteId);
        List<string> files = Directory.GetFiles(_uploadPath).ToList();
        var noteFiles = files.Where(w =>
                                        w.Contains($"Note{NoteId}")
                                        && !w.Contains("Processed"))?.ToList();
        return noteFiles.Count();
    }

    public int NumberOfImagesProcessed(int NoteId)
    {
        _logger.LogInformation("Starting loading the files for Note {id}", NoteId);
        List<string> files = Directory.GetFiles(_uploadPath).ToList();
        var noteFiles = files.Where(w =>
                                        w.Contains($"Note{NoteId}", StringComparison.OrdinalIgnoreCase)
                                        && w.Contains("Processed", StringComparison.OrdinalIgnoreCase))?.ToList();
        return noteFiles.Count();
    }
    private void RemaneFilesToProcessed(string path)
    {
        string suffix = "Processed";
        if (File.Exists(path))
        {
            string file = path;
            _logger.LogInformation($"Renaming file in {path}:");

            string directory = Path.GetDirectoryName(file);
            string fileName = Path.GetFileNameWithoutExtension(file);
            string extension = Path.GetExtension(file);
            string newFileName = $"{fileName}_{suffix}{extension}";
            string newFilePath = Path.Combine(directory, newFileName);

            try
            {
                File.Move(file, newFilePath);
                _logger.LogInformation($"Renamed: {fileName}{extension} to {newFileName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error renaming file {fileName}{extension}: {ex.Message}");
            }
        }
        else
        {
            _logger.LogInformation($"The path file {path} does not exist.");
        }
    }
    private async Task<string> CallAIOcr(string imagePath)
    {
        OpenAIAPI api = new OpenAIAPI(_key);
        var chat = api.Chat.CreateConversation();

        chat.Model = new Model("gpt-4o");
        chat.AppendSystemMessage("You are a OCR system, give the transcription of the image in the format that is on the image. ");
        chat.AppendUserInput("Each line embrace with <div></div> tags");
        chat.AppendUserInput("For example: <div>This is a line extract from the image</div>");
        chat.AppendUserInput("Give me just the transcription of the image and nothing more.", ImageInput.FromFile(imagePath));
        string response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
}
