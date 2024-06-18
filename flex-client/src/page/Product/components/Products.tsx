export interface IProductIndex {
    id: number;
    name: string;
    code: string;
    originPrice: number;
    unitPrice: number;
    image: string;
}

const Products: React.FC = () => {
    return (
        <div>
            <h1>Products</h1>
        </div>
    )
}

export default Products;