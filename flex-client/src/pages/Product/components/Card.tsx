import { IProductIndex } from "../../../hooks/productHook";
import config from "../../../config";
import styles from '../product.module.scss';

const Card: React.FC<{ product: IProductIndex }> = ({ product }) => {
    const baseUrl = config.baseImageUrl;
    const url = `${baseUrl}/${product.productColors[0].productPictures[0].url}`;

    return (
        <div className={`${styles['product-card']}`}>
            <div className={`${styles['product-card__image_container']}`} >
                <img src={url} alt={product.name} className={`${styles['product-card__image']}`} />
            </div>
            <h3>{product.name}</h3>
            <p>{product.unitPrice}元</p>
        </div>
    )
};

export default Card;