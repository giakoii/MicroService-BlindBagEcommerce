# MicroService-BlindBagEcommerce 🛍️🎁

An e-commerce system using Microservices model to manage Blind Bag products and accompanying accessories.

---
## 📦 Main features

- Register / Login / User authentication (OpenID + JWT)
- Product and accessory management
- Blind Box / Blind Bag management
- Cart, wishlist, and orders
- Refund request, chat box, comments, product reviews
- Management system (Admin): browse products, inventory, process return requests
- Calculate service fees, online payment
- Interface for multiple roles (Admin, Staff, Seller, Customer)

## 🧱 System architecture

The project uses **Microservices** architecture, each service handles a separate part:

- `AuthService`: User authentication, JWT token issuance
- `AccessoryService`: Accessory management
- `CartAndWishListService`: Cart and wishlist
- `OrderService`: Order management and refund
- `ChatService`: Messaging
- `ExchangeService`: Blind Bag exchange request processing
- ...

## ⚙️ Technology used

- **Backend**: ASP.NET Core (.NET 9), Entity Framework Core, OpenIddict
- **Database**: SQL Server
- **Authentication**: JWT + OpenIddict
- **Logging**: NLog
- **API Documentation**: Swagger / NSwag
- **Containerization**: Docker + Docker Compose
- **Frontend**: (not yet integrated - will add React/Next.js or mobile app later)