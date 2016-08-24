namespace GTChallenge.Code
{
      /// <summary>
      ///       File record definition
      /// </summary>
      public interface IRecordItem
      {
            string Lastname { get; set; }
            string Firstname { get; set; }
            string Gender { get; set; }
            string Dateofbirth { get; set; }
            string Favorite { get; set; }
      }
}