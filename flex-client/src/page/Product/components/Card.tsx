import { IProductIndex } from "../../../hooks/productHook";
import config from "../../../config";

const Card: React.FC<{ product: IProductIndex }> = ({ product }) => {
    const baseUrl = config.baseImageUrl;
    const url = `${baseUrl}/${product.productColors[0].productPictures[0].url}`;

    return (
        <div className="product-card">
            <img src={url} alt={product.name} style={{ width: '100px' }} />
            <h3>{product.name}</h3>
            <p>{product.unitPrice}元</p>
        </div>
    )
};

export default Card;