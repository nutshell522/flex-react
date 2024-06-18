import { IProductIndex } from "./Products";



const Card: React.FC<{ product: IProductIndex }> = ({ product }) => {
    return (
        <div className="product-card">
            <img src={product.image} alt={product.name} />
            <h3>{product.name}</h3>
            <p>{product.unitPrice}</p>
        </div>
    )
};

export default Card;