import { IProductIndex } from "../../../hooks/productHook";
import Card from "./Card";


const Products: React.FC<{ products: IProductIndex[] }> = ({ products }) => {

    return (
        <div>
            {products.map((product) => (
                <Card key={product.id} product={product} />
            ))}
        </div>
    )
}

export default Products;