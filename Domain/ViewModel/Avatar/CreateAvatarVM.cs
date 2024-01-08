namespace Domain.ViewModel.Avatar
{
    public class CreateAvatarVM
    {
        public CreateAvatarVM()
        {
            file = new MemoryStream();
        }

        public int UserId { get; set; }
        public MemoryStream file { get; set; }
    }
}
