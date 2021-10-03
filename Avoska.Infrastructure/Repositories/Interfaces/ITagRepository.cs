using System.Collections.Generic;

public interface ITagRepository {
    IEnumerable<Tag> GetTags ();

}