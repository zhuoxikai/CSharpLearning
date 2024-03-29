﻿using System;
using System.Collections.Generic;

namespace CSharpProfessional.Extension
{
  internal class Set<TElement>
  {
    private int[] buckets;
    private Set<TElement>.Slot[] slots;
    private int count;
    private int freeList;
    private IEqualityComparer<TElement> comparer;

    public Set()
      : this((IEqualityComparer<TElement>) null)
    {
    }

    public Set(IEqualityComparer<TElement> comparer)
    {
      if (comparer == null)
        comparer = (IEqualityComparer<TElement>) EqualityComparer<TElement>.Default;
      this.comparer = comparer;
      this.buckets = new int[7];
      this.slots = new Set<TElement>.Slot[7];
      this.freeList = -1;
    }

    public bool Add(TElement value) => !this.Find(value, true);

    public bool Contains(TElement value) => this.Find(value, false);

    public bool Remove(TElement value)
    {
      int hashCode = this.InternalGetHashCode(value);
      int index1 = hashCode % this.buckets.Length;
      int index2 = -1;
      for (int index3 = this.buckets[index1] - 1; index3 >= 0; index3 = this.slots[index3].next)
      {
        if (this.slots[index3].hashCode == hashCode && this.comparer.Equals(this.slots[index3].value, value))
        {
          if (index2 < 0)
            this.buckets[index1] = this.slots[index3].next + 1;
          else
            this.slots[index2].next = this.slots[index3].next;
          this.slots[index3].hashCode = -1;
          this.slots[index3].value = default(TElement);
          this.slots[index3].next = this.freeList;
          this.freeList = index3;
          return true;
        }

        index2 = index3;
      }

      return false;
    }

    private bool Find(TElement value, bool add)
    {
      int hashCode = this.InternalGetHashCode(value);
      for (int index = this.buckets[hashCode % this.buckets.Length] - 1; index >= 0; index = this.slots[index].next)
      {
        if (this.slots[index].hashCode == hashCode && this.comparer.Equals(this.slots[index].value, value))
          return true;
      }

      if (add)
      {
        int index1;
        if (this.freeList >= 0)
        {
          index1 = this.freeList;
          this.freeList = this.slots[index1].next;
        }
        else
        {
          if (this.count == this.slots.Length)
            this.Resize();
          index1 = this.count;
          ++this.count;
        }

        int index2 = hashCode % this.buckets.Length;
        this.slots[index1].hashCode = hashCode;
        this.slots[index1].value = value;
        this.slots[index1].next = this.buckets[index2] - 1;
        this.buckets[index2] = index1 + 1;
      }

      return false;
    }

    private void Resize()
    {
      int length = checked(this.count * 2 + 1);
      int[] numArray = new int[length];
      Set<TElement>.Slot[] slotArray = new Set<TElement>.Slot[length];
      Array.Copy((Array) this.slots, 0, (Array) slotArray, 0, this.count);
      for (int index1 = 0; index1 < this.count; ++index1)
      {
        int index2 = slotArray[index1].hashCode % length;
        slotArray[index1].next = numArray[index2] - 1;
        numArray[index2] = index1 + 1;
      }

      this.buckets = numArray;
      this.slots = slotArray;
    }

    internal int InternalGetHashCode(TElement value) =>
      (object) value != null ? this.comparer.GetHashCode(value) & int.MaxValue : 0;

    internal struct Slot
    {
      internal int hashCode;
      internal TElement value;
      internal int next;
    }
  }

}