import { useEffect, useState } from "react";
import { ProductApi } from "../utils/api";
import { IProductIndex } from "../page/Product/components/Products";

const useProductAPI = () => {
    const [products, setProducts] = useState<IProductIndex[]>([]);

    useEffect(() => {
        ProductApi.get().then((data) => setProducts(data.data.content));
    }, []);

    return { products };
};

export default useProductAPI;