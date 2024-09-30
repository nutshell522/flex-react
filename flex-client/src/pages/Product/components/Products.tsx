import { IProductIndex } from "../../../hooks/productHook";
import Card from "./Card";
import styles from '../product.module.scss';


const Products: React.FC<{ products: IProductIndex[] }> = ({ products }) => {

    return (
        <div className={`${styles['products']}`}>
            {products.map((product) => (
                <Card key={product.id} product={product} />
            ))}
        </div>
    )
}

export default Products;