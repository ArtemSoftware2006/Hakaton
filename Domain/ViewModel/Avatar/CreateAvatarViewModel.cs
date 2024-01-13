namespace Domain.ViewModel.Avatar
{
    public class CreateAvatarViewModel
    {
        public CreateAvatarViewModel()
        {
            file = new MemoryStream();
        }

        public int UserId { get; set; }
        public MemoryStream file { get; set; }
    }
}
