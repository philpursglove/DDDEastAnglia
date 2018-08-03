using System.ComponentModel.DataAnnotations;

namespace DDDEastAnglia.Models
{
    public class BagFile
    {
        [Key]
        public int BagFileId { get; set; }

        public int ContentId { get; set; }

        public string Filename { get; set; }

        public string MIMEType { get; set; }

        public byte[] Bytes { get; set; }

        public int FileLength { get; set; }
    }
}