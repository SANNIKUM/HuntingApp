namespace AFFHA.Domain
{
    using System;
    public class BaseEntityTrackable : BaseEntity
    {
        /// <summary>
        /// Datetime when an entity is created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identity of person who created the entity.
        /// Defaults to "Unknown" if not identifiable with JWT.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Datetime when an entity is modified.
        /// </summary>
        public DateTimeOffset ModifiedAt { get; set; }

        /// <summary>
        /// Identity of person who motified the entity.
        /// Defaults to "Unknown" if not identifiable with JWT.
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}