﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnofficialCrusaderPatch
{
    public class ChangeHeader : IEnumerable<ChangeEdit>
    {
        LabelCollection labels = new LabelCollection();
        public LabelCollection Labels => labels;

        protected readonly List<ChangeEdit> editList = new List<ChangeEdit>();

        public IEnumerator<ChangeEdit> GetEnumerator() => editList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public virtual void Add(ChangeEdit edit)
        {
            editList.Add(edit);
            edit.SetParent(this);
        }

        public void Activate(ChangeArgs args)
        {
            if (!editList.TrueForAll(e => e.Initialize(args)))
                return;

            editList.ForEach(e => e.Activate(args));
        }
    }
}
