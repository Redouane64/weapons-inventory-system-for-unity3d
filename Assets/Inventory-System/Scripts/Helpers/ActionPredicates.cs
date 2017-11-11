using System.Collections.Generic;
using System;

namespace WeaponsInventorySystem.Helpers
{
    public class ActionPredicates
    {
        private readonly List<Func<bool>> predicates;

        public ActionPredicates()
        {
            predicates = new List<Func<bool>>();
        }

        public void AddPredicate(Func<bool> predicate)
        {
            if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}

			predicates.Add(predicate);
        }

        public bool CanExecuteAction()
        {
            for (int i = 0; i < predicates.Count; i++)
			{
				if (!predicates[i]())
				{
					return false;
				}
			}

			return true;
        }
    } 
}