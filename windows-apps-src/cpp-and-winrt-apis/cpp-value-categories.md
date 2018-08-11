---
author: stevewhims
description: This topic describes the various categories of values that exist in C++. You will doubtless have heard of lvalues and rvalues, but there are other kinds, too.
title: Value categories, and references to them
ms.author: stwhi
ms.date: 08/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, moving, forwarding, value categories, move semantics, perfect forwarding, lvalue, rvalue, glvalue, prvalue, xvalue
ms.localizationpriority: medium
---

# Value categories, and references to them
This topic describes the various categories of values (and references to values) that exist in C++. You will doubtless have heard of *lvalues* and *rvalues*, but you may not think of them in the terms that this topic presents. And there are other kinds of values, too.

Every expression in C++ yields a value that belongs to one of the categories discussed in this topic. There are aspects of the C++ language, its facilies, and rules, that demand a proper understanding of these value categories. For example, taking the address of a value, copying a value, moving a value, and forwarding a value on to another function. This topic doesn't go into all of those aspects in depth, but it provides foundational information for a solid understanding of them.

The info in this topic is framed in terms of Stroustrup's analysis of value categories by the two independent properties of identity and movability [Stroustrup, 2013].

## An lvalue has identity
What does it mean for a value to have *identity*? If you have (or you can take) the memory address of a value and use it safely, then the value has identity. That way, you can do more than compare the contents of values: you can compare or distinguish them by identity.

An *lvalue* has identity. It's now a matter of only historical interest that the "l" in "lvalue" is an abbreviation of "left" (as in, the left-hand-side of an assignment). In C++, an lvalue can appear on the left *or* on the right of an assignment. The "l" in "lvalues", then, doesn't actually help you to comprehend nor define what they are. You need only to understand that what we call an lvalue is a value that has identity.

Examples of expressions that are lvalues include: a named variable or constant, or a function that returns a reference. Examples of expressions that are *not* lvalues include: a temporary, or a function that returns by value.

```cppwinrt
int& get_by_ref() { ... }
int get_by_val() { ... }

int main()
{
    std::vector<byte> vec{ 99, 98, 97 };
    std::vector<byte>* addr1{ &vec }; // ok: vec is an lvalue.
    int* addr2{ &get_by_ref() }; // ok: get_by_ref() is an lvalue.

    int* addr3{ &(get_by_ref() + 1) }; // error: get_by_ref() + 1 is not an lvalue.
    int* addr4{ &get_by_val() }; // error: get_by_val() is not an lvalue.
}
```

Now, while it's a true statement that lvalues have identity, so do xvalues. We'll go a little more into what an *xvalue* is later in this topic. For now, just be aware that there is a value category called glvalue, for "generalized lvalue". The superset of glvalues contains both lvalues (also known as *classical lvalues*) and xvalues. So, while "an lvalue has identity" is true, the complete set of things that have identity is the set of glvalues, as shown in this illustration.

![An lvalue has identity](images/has-identity1.png)

## An rvalue is movable; an lvalue is not
But there are values that are not glvalues. Consequently, there are values that you *can't* obtain a memory address for (or you can't rely on it to be valid). We saw some such values in the code example above. This sounds like a disadvantage. But in fact the advantage of a value like that is that you can *move* it (which is generally cheap), rather than copy it (which is generally expensive). Moving a value means that it's no longer in the place it used to be. So, trying to access it in the place it used to be is something to be avoided. A discussion of when and *how* to move a value is out of scope for this topic. For this topic, we just need to know that a value that is movable is known as an *rvalue* (or *classical rvalue*).

The "r" in "rvalue" is an abbreviation of "right" (as in, the right-hand-side of an assignment). But you can use rvalues, and references to rvalues, outside of assignments. The "r" in "rvalues", then, is not the thing to focus on. You need only to understand that what we call an rvalue is a value that is movable.

An lvalue, conversely, isn't movable, as shown in this illustration. You can't move an lvalue because, if you could, then it'd be unsafe (or even disastrous) to continue to access it afterward. Remember, you have its identity.

![An rvalue is movable; an lvalue is not](images/is-movable.png)

You can't move an lvalue. But there *is* a kind of glvalue (the set of things with identity) that you can move&mdash;if you know what you're doing&mdash;and that's the xvalue. We'll revisit this idea one more time below, when we look at the complete picture of value categories.

Let's close this section with a look at the syntax for a reference to an rvalue. We'll have to wait for another topic to go into a substantial treatment of moving and forwarding, but those are problems that are solved by rvalue references. Before we look at rvalue references, though, we first need to be clearer about `T&`&mdash;the thing we've formerly been calling just "a reference". It's really "an lvalue (non-const) reference", which refers to an object to which the user of the reference can write.

```cppwinrt
template<typename T> T& get_by_lvalue_ref() { ... } // Get by lvalue (non-const) reference.
template<typename T> void set_by_lvalue_ref(T&) { ... } // Set by lvalue (non-const) reference.
```

And then there are lvalue const references (`T const&`), which refer to objects to which the user of the reference can't write (for example, a constant).

```cppwinrt
template<typename T> T const& get_by_lvalue_cref() { ... } // Get by lvalue const reference.
template<typename T> void set_by_lvalue_cref(T const&) { ... } // Set by lvalue const reference.
```

Whereas the syntax for a reference to an rvalue of type `T` is written as `T&&`. An rvalue reference refers to an object whose value we don't need to preserve after we've used it (for example, a temporary). Since the whole point is to modify them, `const` and `volatile` qualifiers (also known as cv-qualifiers) don't apply to rvalue references.

```cppwinrt
template<typename T> T&& get_by_rvalue_ref() { ... } // Get by rvalue reference.
struct A { A(A&& other) { ... } }; // A move constructor takes an rvalue reference.
```

Because an rvalue reference refers to an object whose value it's assumed we don't need to preserve, an rvalue reference (say, the parameter for a move constructor) can bind to an rvalue, but not to an lvalue.

## An lvalue has identity; a prvalue does not
At this stage, we know what has identity. And we know what's movable and what isn't. But we haven't yet named the set of values that *don't* have identity. That set is known as the *prvalue*, or *pure rvalue*.

```cppwinrt
int& get_by_ref() { ... }
int get_by_val() { ... }

int main()
{
    int* addr3{ &(get_by_ref() + 1) }; // error: get_by_ref() + 1 is a prvalue.
    int* addr4{ &get_by_val() }; // error: get_by_val() is a prvalue.
}
```

![An lvalue has identity; a prvalue does not](images/has-identity2.png)

## The complete picture of value categories
It only remains to combine the info and illustrations above into a single, big picture.

![The complete picture of value categories](images/value-categories.png)

### glvalue (i)
A glvalue (generalized lvalue) has identity.

### lvalue (i\&\!m)
An lvalue (a kind of glvalue) has identity, but isn't movable. These are typically read-write values that you pass around by reference or by const reference, or by value if copying is cheap. An lvalue can't be bound to an rvalue reference.

### xvalue (i\&m)
An xvalue (a kind of glvalue, but also a kind of rvalue) has identity, and is also movable. This might be an erstwhile lvalue that you've decided to move because copying is expensive, and you'll be careful not to access it afterward. Here's how you can turn an lvalue into an xvalue.

```cppwinrt
struct A { ... };
A a; // a is an lvalue...
static_cast<A&&>(a); // ...but this expression is an xvalue.
```

In the code example above, we haven't moved anything yet. We've just created an xvalue by casting an lvalue to an rvalue reference. It can still be identified by its lvalue name; but, as an xvalue, it is now *capable* of being moved. The reasons for doing so, and what moving actually looks like, will have to wait for another topic. But you can think of the "x" in "xvalue" as meaning "expert-only" if that helps. Here's another example of an xvalue&mdash;calling a function that returns an rvalue reference.

```cppwinrt
struct A { int m; };
A&& f();
f(); // This expression is an xvalue...
f().m; // ...and so is this.
```

### prvalue (\!i\&m)
A prvalue (pure rvalue; a kind of rvalue) doesn't have identity, but is movable. These are typically literals, temporaries, return values&mdash;anything that's the result of evaluating an expression (an expression that's not a glvalue), or anything returned by value from a function.

### rvalue (m)
An rvalue is movable. An rvalue prefers to be bound to an rvalue reference, but it can be bound to an lvalue const reference. An rvalue *reference* always refers to an rvalue (an object whose value it's assumed we don't need to preserve).

But, is an rvalue reference itself an rvalue? An *unnamed* rvalue reference (like the ones shown in the xvalue code examples above) is an xvalue so, yes, it's an rvalue. It prefers to be bound to an rvalue reference function parameter, such as that of a move constructor. Conversely (and perhaps counter-intuitively), if an rvalue reference has a name, then the expression consisting of that name is an lvalue. So it *can't* be bound to an rvalue reference parameter. But it's easy to make it do so&mdash;just cast it to an unnamed rvalue reference (an xvalue) again.

```cppwinrt
void foo(A&) { ... }
void foo(A&&) { ... }
void bar(A&& a) // a is a named rvalue reference; it's an lvalue.
{
    foo(a); // Calls foo(A&).
    foo(static_cast<A&&>(a)); // Calls foo(A&&).
}
A&& get_by_rvalue_ref() { ... } // This unnamed rvalue reference is an xvalue.
```

### \!i\&\!m
The kind of value that doesn't have identity and isn't movable is the one combination that we haven't yet discussed. But we can disregard it, because that category isn't a useful idea in the C++ language.

## References
* \[Stroustrup, 2013\] B. Stroustrup: The C++ Programming Language, Fourth Edition. Addison-Wesley. 2013.