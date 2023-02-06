﻿using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity
{
    internal Order()
    {
        Email = string.Empty;
        DeliveryAddress = string.Empty;
    }

    private Order(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime? dateCancelled)
        : base(id)
    {
        Email = email;
        DeliveryAddress = deliveryAddress;
        DateCancelled = dateCancelled;
        IsCancelled = dateCancelled is not null;
        CreationDate = DateTime.UtcNow;
    }

    public string Email { get; private set; }
    public string DeliveryAddress { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? DateCancelled { get; private set; }
    public bool IsCancelled { get; private set; }
    public IReadOnlyList<OrderItem>? Items { get; private set; }

    public static Order Create(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime? dateCancelled = null,
        IReadOnlyList<OrderItem>? items = null)
    {
        var order = new Order(
            id,
            email,
            deliveryAddress,
            dateCancelled)
        {
            Items = items
        };

        return order;
    }
}