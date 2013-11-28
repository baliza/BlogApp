
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public abstract class Entity : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the date and time the entity was created.
        /// </summary>
        /// <value>The creation date and time.</value>
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the entity ID.
        /// </summary>
        /// <value>The entity ID.</value>
        public virtual String Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was last updated.
        /// </summary>
        /// <value>The last-update date and time, or null.</value>
        public virtual DateTime? UpdatedOn { get; set; }

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
            Id = Guid.Empty.ToString();
        }

        #endregion

        #region Members

        /// <summary>
        /// Gets a JSON serialisation of this instance.
        /// </summary>
        /// <returns>A <see cref="String"/> representing this instance.</returns>
        public virtual string ToJson()
        {
            throw new NotImplementedException();
            //return JsonHelper.ToJson(this);
        }

        #endregion
    }
}
